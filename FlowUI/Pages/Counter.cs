using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLowUI.Pages
{
    public partial class Counter
    {
        private int CurrentCount = 0;

        public Counter() {}

        protected override async Task OnInitializedAsync()
        {
            CurrentCount = 0;
        }

        private void IncrementCount()
        {
            CurrentCount++;
        }
    }
}
