using System;

namespace TestingLoveAndHate {
    internal class Something {
        private long employeeId;
        private string employeeStatus;
        private int daysSoFar;

        public Something(long employeeId, string employeeStatus, int daysSoFar) {
            this.employeeId = employeeId;
            this.employeeStatus = employeeStatus;
            this.daysSoFar = daysSoFar;
        }

        internal Result RequestDaysOff(int days) {
            if (daysSoFar + days > 26) {
                if (employeeStatus == "PERFORMER" && daysSoFar + days < 45) {
                    return Result.Manual;
                } else {
                    return Result.Denied;
                }
            } else {
                if (employeeStatus == "SLACKER") {
                    return Result.Denied;
                } else {
                    return Result.Approved;
                }
            }
        }
    }
}