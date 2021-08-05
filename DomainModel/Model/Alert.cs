namespace DomainModel
{
    /// <summary>
    /// Alerts are shown in UserDashboard to indicate to the user if he has 
    /// been assigned to a job, or perhaps mentioned in a conversation. 
    /// 
    /// 
    /// Project Wide alerts are shown to every user in the project
    /// on the left hand side of their dashboard. The purpose of this is
    /// to make announcements or give reminders to everyone. For instance
    /// "Reminder, monday is a holiday!" or "Remember to update your current
    /// asset's status!"
    /// 
    /// </summary>
    public partial class Alert : DatabaseBackedObject
    {
        public override int ID
        {
            get => ar_id;
            set => ar_id = value;
        }

        public override string Name
        {
            get => ar_header;
            set => ar_header = value;
        }

    }
}
