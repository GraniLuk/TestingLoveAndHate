namespace TestingLoveAndHate {
    public interface ILeaveDatabase {
        object[] FindByEmployeeId(long employeeId);
        void Save(object[] employeeData);
    }
}