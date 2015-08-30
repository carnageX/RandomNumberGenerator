using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RandomNumberGenerator
{
    class MainViewModel : ViewModelBase
    {
        public List<int> CountRange { get; set; }
        private const int GROUPS = 3;
        //input box to get the amount of numbers (1 - 38) to generate.
        //Then get the random numbers displayed in 3 different rows.
        //Then scan row 2 and row 3 for integers between 1 and 12.
        //Then display the amount of integers between those values.

        public MainViewModel()
        {
            CountRange = PopulateCountRange(1, 12);
        }

        public int NumsToGenerate
        {
            get { return _numsToGenerate; }
            set
            {
                if(_numsToGenerate != value)
                {
                    _numsToGenerate = value;
                    RaisePropertyChanged("NumsToGenerate");
                }
            }
        }
        private int _numsToGenerate;

        public string RandomNumListAlpha
        {
            get { return _randomNumListAlpha; }
            set
            {
                if(_randomNumListAlpha != value)
                {
                    _randomNumListAlpha = value;
                    RaisePropertyChanged("RandomNumListAlpha");
                }
            }
        }
        private string _randomNumListAlpha;

        public string RandomNumListBravo
        {
            get { return _randomNumListBravo; }
            set
            {
                if(_randomNumListBravo != value)
                {
                    _randomNumListBravo = value;
                    RaisePropertyChanged("RandomNumListBravo");
                }
            }
        }
        private string _randomNumListBravo;

        public string RandomNumListCharlie
        {
            get { return _randomNumListCharlie; }
            set
            {
                if(_randomNumListCharlie != value)
                {
                    _randomNumListCharlie = value;
                    RaisePropertyChanged("RandomNumListCharlie");
                }
            }
        }
        private string _randomNumListCharlie;

        public int BravoListCount
        {
            get { return _bravoListCount; }
            set
            {
                if(_bravoListCount != value)
                {
                    _bravoListCount = value;
                    RaisePropertyChanged("BravoListCount");
                }
            }
        }
        private int _bravoListCount;

        public int CharlieListCount
        {
            get { return _charlieListCount; }
            set
            {
                if(_charlieListCount != value)
                {
                    _charlieListCount = value;
                    RaisePropertyChanged("CharlieListCount");
                }
            }
        }
        private int _charlieListCount;

        public int BravoCharlieCount
        {
            get { return _bravoCharlieCount; }
            set
            {
                if(_bravoCharlieCount != value)
                {
                    _bravoCharlieCount = value;
                    RaisePropertyChanged("BravoCharlieCount");
                }
            }
        }
        private int _bravoCharlieCount;

        public ICommand CmdGenerate
        {
            get
            {
                if (_cmdGenerate == null)
                    _cmdGenerate = new RelayCommand(Generate, CanGenerate);
                return _cmdGenerate;
            }
        }
        private RelayCommand _cmdGenerate;

        private bool CanGenerate()
        {
            return true;
        }

        private void Generate()
        {
            //populate Alpha, Bravo, Charlie with random nums, 1-38
            //count nums between 1 - 12 in Bravo & Charlie
            var rList = PopulateRandomList(1, 38, NumsToGenerate);
            List<List<int>> rlistCollection = new List<List<int>>();
            var numsPerGroup = (NumsToGenerate / GROUPS) + 1;
            for (int i = 0; i < GROUPS; i++)
            {
                rlistCollection.Add(rList.Skip(i * numsPerGroup).Take(numsPerGroup).ToList());
            }
            rlistCollection[0].Sort();
            rlistCollection[1].Sort();
            rlistCollection[2].Sort();
            RandomNumListAlpha = String.Join(", ", rlistCollection[0]);
            RandomNumListBravo = String.Join(", ", rlistCollection[1]);
            RandomNumListCharlie = String.Join(", ", rlistCollection[2]);

            BravoListCount = Count(rlistCollection[1]);
            CharlieListCount = Count(rlistCollection[2]);
            BravoCharlieCount = BravoListCount + CharlieListCount;
        }

        private int Count(List<int> inputList)
        {
            int count = 0;
            foreach(var item in inputList)
            {
                if(CountRange.Contains(item))
                {
                    count++;
                }
            }
            return count;
        }

        private List<int> PopulateRandomList(int start, int finish, int size)
        {
            List<int> rList = new List<int>();
            Random r = new Random();
            int rInt;
            for(int i = 0; i < size; i++)
            {
                rInt = r.Next(start, finish);
                rList.Add(rInt);
            }
            return rList;
        }

        private List<int> PopulateCountRange(int start, int finish)
        {
            return Enumerable.Range(start, finish).ToList();
        }
    }
}
