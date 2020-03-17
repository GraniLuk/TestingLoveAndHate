using System;

namespace TestingLoveAndHate {
    public class Something : IEquatable<Something> {
        private long employeeId;
        private string employeeStatus;
        private int daysSoFar;

        public Something(long employeeId, string employeeStatus, int daysSoFar) {
            this.employeeId = employeeId;
            this.employeeStatus = employeeStatus;
            this.daysSoFar = daysSoFar;
        }

        public Result RequestDaysOff(int days) {
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
                    daysSoFar += days;
                    return Result.Approved;
                   
                }
            }
        }

        public bool Equals(Something other) {
            return employeeId == other.employeeId &&
            employeeStatus == other.employeeStatus &&
            daysSoFar == other.daysSoFar;
        }
    }
}