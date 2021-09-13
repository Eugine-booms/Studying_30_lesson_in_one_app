using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonInOne.DelegateAndEvents
{
    public class Lesson
    {
        //public event делегат название
        public event Action RingIsCalling;
        public event EventHandler<DateTime> Started; //Коротко и со вкусом
        /// <summary>
        /// Тут нужно создать класс наследник EventArgs() например ShootingEventArgs и передать его Bang?.Invoke(this, new ShootingEventArgs());
        /// </summary>
        public event EventHandler<ShootingEventArgs> Bang;              //Bang?.Invoke(this, new EventArgs()); 
        public DateTime Begin { get; private set; }
        public DateTime End { get; private set; }
        public string Name { get; set; }

        public Lesson(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
        public void Start()
        {
            Begin = DateTime.Now;
            Started?.Invoke(this, Begin);
        }
        public void CallRing()
        {
            End = DateTime.Now;
            RingIsCalling?.Invoke();
        }
        public void BangBang()
        {
            var args = new ShootingEventArgs(DateTime.Now, 6);
            Bang?.Invoke(this, args);
        }
        public override string ToString()
        {
            return Name;
        }

    }
   public class ShootingEventArgs : EventArgs
    {
        public ShootingEventArgs(DateTime start, int shootCount)
        {
            Start = start;
            ShootCount = shootCount;
        }

        public DateTime Start { get; set; }
        public int ShootCount { get; set; }
    }
}
