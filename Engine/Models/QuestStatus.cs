namespace Engine.Models
{
    public class QuestStatus : BaseNotificationClass
    {
        private bool isCompleted;

        public Quest PlayerQuest { get; }// added set; Guessing will not work without it
        public bool IsCompleted
        {
            get { return isCompleted; }
            set
            {
                isCompleted = value;
                OnPropertyChanged();
            }
        }

        public QuestStatus(Quest quest)
        {
            PlayerQuest = quest;
            IsCompleted = false;
        }
    }
}
