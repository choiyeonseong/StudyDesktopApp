using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfPracticeApp.BusinessLogic
{
    public class Car
    {
        public double Speed { get; set; }   // 속성(프로퍼티) : 대문자로 시작 
        public Color MainColor { get; set; }  // 키워드는 중복사용 안하는게 좋음
        public Human Driver { get; set; }
    }
    public class Human
    {
        public bool HasDriveLicense { get; set; }
    }
}
