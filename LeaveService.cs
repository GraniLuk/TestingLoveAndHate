using System;

namespace TestingLoveAndHate {
    public class LeaveService {
        private readonly ILeaveDatabase _database;
        private readonly IMessageBus _messageBus;
        private readonly IEscalationManager _escalationManager;
        private readonly IEmailSender _emailSender;

        public LeaveService(ILeaveDatabase database, IMessageBus messageBus, IEmailSender emailSender, IEscalationManager escalationManager) {
            _database = database;
            _messageBus = messageBus;
            _escalationManager = escalationManager;
            _emailSender = emailSender;
        }

        public Result RequestPaidDaysOff(int days, long employeeId) {
            validate(days);

            //load
            var something = _database.FindByEmployeeId(employeeId);

            Result result = something.RequestDaysOff(days);

            switch (result) {
                case Result.Manual:
                    _escalationManager.NotifyNewPendingRequest(employeeId);
                    break;
                case Result.Approved:
                    _database.Save(something);
                    _messageBus.SendEvent("Request approved");
                    break;
                case Result.Denied:
                    _emailSender.Send("Next time!");
                    break;
            }

            return result;
        }

        private static void validate(int days) {
            if (days < 0) {
                throw new ArgumentException();
            }
        }
    }
}
