using System;
using ContosoFieldService.Models;
using FreshMvvm;
using MvvmHelpers;

namespace ContosoFieldService.PageModels
{
    public class PartsPageModel : FreshBasePageModel
    {
        public ObservableRangeCollection<Part> Parts { get; set; }

        public override void Init(object initData)
        {
            base.Init(initData);

            Parts = new ObservableRangeCollection<Part>();
        }


    }
}
