using System.Collections.ObjectModel;
using FreshMvvm;

namespace ContosoFieldService.PageModels
{
    public class ProfilePageModel : FreshBasePageModel
    {
        public ObservableCollection<Stats> Statistics { get; set; }

        public override void Init(object initData)
        {
            base.Init(initData);

            Statistics = new ObservableCollection<Stats>();

            Statistics.Add(new Stats { Title = "Jobs Completed", Label1 = "August", Label2 = "July", Value1 = "203", Value2 = "434" });
            Statistics.Add(new Stats { Title = "Average Pace (Min/Km)", Label1 = "August", Label2 = "July", Value1 = "4:34", Value2 = "5:02" });
            Statistics.Add(new Stats { Title = "Activities", Label1 = "August", Label2 = "July", Value1 = "1", Value2 = "6" });
            Statistics.Add(new Stats { Title = "Calories Burned", Label1 = "August", Label2 = "July", Value1 = "341", Value2 = "1.954" });
            Statistics.Add(new Stats { Title = "Elevation Climb (M)", Label1 = "August", Label2 = "July", Value1 = "29,3", Value2 = "221,1" });
            Statistics.Add(new Stats { Title = "Time Spent", Label1 = "August", Label2 = "July", Value1 = "19:22", Value2 = "2:02:39" });
        }
    }

    public class Stats
    {
        public string Title { get; set; }
        public string Value1 { get; set; }
        public string Label1 { get; set; }
        public string Value2 { get; set; }
        public string Label2 { get; set; }
    }
}
