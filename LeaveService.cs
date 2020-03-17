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
            if (days < 0) {
                throw new ArgumentException();
            }

            object[] employeeData = _database.FindByEmployeeId(employeeId);

            string employeeStatus = (string)employeeData[0];

            int daysSoFar = (int)employeeData[1];

            Result result;
            if (daysSoFar + days > 26) {
                if (employeeStatus == "PERFORMER" && daysSoFar + days < 45) {
                    result = Result.Manual;
                    _escalationManager.NotifyNewPendingRequest(employeeId);
                } else {
                    result = Result.Denied;
                    _emailSender.Send("Next time");
                }
            } else {
                if (employeeStatus == "SLACKER") {
                    result = Result.Denied;
                    _emailSender.Send("Next time");
                } else {
                    employeeData[1] = daysSoFar + days;
                    result = Result.Approved;
                    _database.Save(employeeData);
                    _messageBus.SendEvent("Request approved");
                }
            }

            return result;
        }
    }
}
