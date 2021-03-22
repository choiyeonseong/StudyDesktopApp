using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfPracticeApp.BusinessLogic
{
    public class Car : Notifier
    {
        //public double Speed { get; set; } // 아래 것과 같음
        private double speed;
        public double Speed
        {
            get { return speed; }
            set
            {
                if (value > 350)
                {
                    speed = 350;
                }
                else
                {
                    speed = value;
                }
                OnPropertyChanged("Speed"); // 속성값 변경된 것을 프로그램에게 알려줌
            }
        }

        private Color mainColor;
        public Color MainColor
        {
            get { return mainColor; }
            set
            {
                mainColor = value;
                OnPropertyChanged("MainColor");
            }
        }  // 키워드는 중복사용 안하는게 좋음

        private Human driver;
        public Human Driver
        {
            get { return driver; }
            set
            {
                driver = value;
                OnPropertyChanged("Driver");
            }
        }
    }
    public class Human
    {
        public string Name { get; set; }
        public bool HasDriveLicense { get; set; }
    }
}
