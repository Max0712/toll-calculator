using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Toll_Calculator_API.Services;
using Nager.Date;
using Toll_Calculator_API.Models;
using Toll_Calculator_API.DbModels;

namespace Toll_Calculator_API.Calculations
{
    public interface ITollCalulations
    {
        Task<VehicleTollSummary> CalculateTollSummary(IEnumerable<VehicleTollEvent> vehicleTollEvents, DateTime fromDate, DateTime toDate);
    }

    public class TollCalculations : ITollCalulations
    {
        private const decimal _dailyLimit = 60M;

        private ITollService _tollService;
        private static IEnumerable<TollFee> _tollFees;

        public TollCalculations(ITollService tollService)
        {
            _tollService = tollService;
        }

        public async Task<VehicleTollSummary> CalculateTollSummary(IEnumerable<VehicleTollEvent> vehicleTollEvents, DateTime fromDate, DateTime toDate)
        {
            if (_tollFees == null || !_tollFees.Any())
                await GetTollFees();

            var publicHolidays = DateSystem.GetPublicHoliday(fromDate, toDate, CountryCode.SE);

            var tolls = vehicleTollEvents.Where(e => !publicHolidays.Any(ph => e.EventTime.Date == ph.Date)
                && e.EventTime.DayOfWeek != DayOfWeek.Saturday && e.EventTime.DayOfWeek != DayOfWeek.Sunday)
                .Select(v => AsTollPairs(v.EventTime))
                .OrderBy(t => t.Key)
                .ToList();

            if (tolls == null || !tolls.Any())
                return new VehicleTollSummary();

            var resultList = new List<KeyValuePair<string, decimal>>();

            var dayTotal = 0M;
            var lastTime = new DateTime();
            foreach (var tollEvent in tolls)
            {
                if (tollEvent.Key <= lastTime)
                    continue;

                if (lastTime.Date != tollEvent.Key.Date)
                    dayTotal = 0;

                if (dayTotal >= _dailyLimit)
                    lastTime = new DateTime(lastTime.Year, lastTime.Month, lastTime.Day, 0, 0, 0).AddDays(1);

                var eventsWithinHour = tolls.Where(x => x.Key >= tollEvent.Key && x.Key <= tollEvent.Key.AddHours(1));
                lastTime = eventsWithinHour.Last().Key;                

                var fee = eventsWithinHour.OrderByDescending(k => k.Value).First().Value;

                if ((dayTotal + fee) > _dailyLimit)
                    fee = _dailyLimit - dayTotal;

                if (fee == 0)
                    continue;

                resultList.Add(new KeyValuePair<string, decimal>(lastTime.ToShortDateString(), fee));

                dayTotal += fee;
                if (dayTotal >= _dailyLimit)
                    dayTotal = _dailyLimit;
            }

            var result = new VehicleTollSummary
            {
                ResultList = resultList,
                TotalCost = resultList.Sum(x => x.Value)
            };

            return result;
        }

        private async Task GetTollFees()
        {
            var getTollFees = _tollService.SelectAllTollFees();
            _tollFees = await getTollFees;

            return;
        }

        private KeyValuePair<DateTime, decimal> AsTollPairs(DateTime tollTime)
        {
            var fee = _tollFees.FirstOrDefault(t => t.From.TimeOfDay <= tollTime.TimeOfDay && t.To.TimeOfDay >= tollTime.TimeOfDay)?.Fee;
            if (fee == null)
                fee = 0M;

            return new KeyValuePair<DateTime, decimal>(tollTime, fee.Value);
        }
    }
}
