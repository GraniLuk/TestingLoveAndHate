using System;

namespace TestingLoveAndHate {
    public class LeaveDatabase : ILeaveDatabase {
        public object[] FindByEmployeeId(long employeeId) {
            throw new NotImplementedException();
        }

        public void Save(object[] employeeData) {
            throw new NotImplementedException();
        }
    }
}