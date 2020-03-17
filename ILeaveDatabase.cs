namespace TestingLoveAndHate {
    public interface ILeaveDatabase {
        Something FindByEmployeeId(long employeeId);
        void Save(Something employeeData);
    }
}