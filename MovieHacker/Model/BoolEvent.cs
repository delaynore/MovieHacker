using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieHacker.Model
{
    public class BoolEvent
    {
        public BoolEvent()
        {
            Variable = false;
        }
        public event Action? ForDo;
        private bool variable;

        public bool Variable
        {
            get { return variable; }
            set 
            {
                variable = value;
                if(variable)
                    ForDo?.Invoke();
            }
        }
    }
}
