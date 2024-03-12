using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab10Lib;

namespace Showcase
{
    public class TestCollections
    {
        private readonly Stack<Button> col1 = [];
        private readonly Stack<string> scol1 = [];

        private readonly SortedDictionary<ControlElement, Button> col2 = [];
        private readonly SortedDictionary<string, Button> scol2 = [];

        private const int stLen = 1000;
        public const int numTests = 10;
        private readonly Stopwatch sw = new();

        private readonly Button start;
        private readonly Button mid;
        private readonly Button end;
        private readonly Button notExist;

        public TestCollections()
        {
            Fill();
            start = (Button)col2.ElementAt(0).Value.Clone();
            mid = (Button)col2.ElementAt(499).Value.Clone();
            end = (Button)col2.ElementAt(999).Value.Clone();
            notExist = new(1000, 1000, "This button does not exist");
        }

        public long[] MeasureStart()
        {
            long[] time = new long[6];
            string strStart = start.ToString();
            ControlElement stBase = start.GetBase;
            string strStBase = stBase.ToString();

            for(int i = 0; i< numTests; i++)
            {
                sw.Restart();
                col1.Contains(start);
                sw.Stop();
                time[0] += sw.ElapsedTicks;

                sw.Restart();
                scol1.Contains(strStart);
                sw.Stop();
                time[1] += sw.ElapsedTicks;

                sw.Restart();
                col2.ContainsValue(start);
                sw.Stop();
                time[2] += sw.ElapsedTicks;

                sw.Restart();
                scol2.ContainsValue(start);
                sw.Stop();
                time[3] += sw.ElapsedTicks;

                sw.Restart();
                col2.ContainsKey(stBase);
                sw.Stop();
                time[4] += sw.ElapsedTicks;

                sw.Restart();
                scol2.ContainsKey(strStBase);
                sw.Stop();
                time[5] += sw.ElapsedTicks;
            }

            return time;
        }

        public long[] MeasureMid()
        {
            long[] time = new long[6];
            string strMid = mid.ToString();
            ControlElement midBase = mid.GetBase;
            string strMidBase = midBase.ToString();

            for (int i = 0; i < numTests; i++)
            {
                sw.Restart();
                col1.Contains(mid);
                sw.Stop();
                time[0] += sw.ElapsedTicks;

                sw.Restart();
                scol1.Contains(strMid);
                sw.Stop();
                time[1] += sw.ElapsedTicks;

                sw.Restart();
                col2.ContainsValue(mid);
                sw.Stop();
                time[2] += sw.ElapsedTicks;

                sw.Restart();
                scol2.ContainsValue(mid);
                sw.Stop();
                time[3] += sw.ElapsedTicks;

                sw.Restart();
                col2.ContainsKey(midBase);
                sw.Stop();
                time[4] += sw.ElapsedTicks;

                sw.Restart();
                scol2.ContainsKey(strMidBase);
                sw.Stop();
                time[5] += sw.ElapsedTicks;
            }
            return time;
        }

        public long[] MeasureEnd()
        {
            long[] time = new long[6];
            string strEnd = end.ToString();
            ControlElement endBase = end.GetBase;
            string strEndBase = endBase.ToString();

            for (int i = 0; i < numTests; i++)
            {
                sw.Restart();
                col1.Contains(end);
                sw.Stop();
                time[0] += sw.ElapsedTicks;

                sw.Restart();
                scol1.Contains(strEnd);
                sw.Stop();
                time[1] += sw.ElapsedTicks;

                sw.Restart();
                col2.ContainsValue(end);
                sw.Stop();
                time[2] += sw.ElapsedTicks;

                sw.Restart();
                scol2.ContainsValue(end);
                sw.Stop();
                time[3] += sw.ElapsedTicks;

                sw.Restart();
                col2.ContainsKey(endBase);
                sw.Stop();
                time[4] += sw.ElapsedTicks;

                sw.Restart();
                scol2.ContainsKey(strEndBase);
                sw.Stop();
                time[5] += sw.ElapsedTicks;
            }
            return time;
        }

        public long[] MeasureNotExist()
        {
            long[] time = new long[6];
            string strNotExist = notExist.ToString();
            ControlElement notExistBase = notExist.GetBase;
            string strNotExistBase = notExistBase.ToString();

            for (int i = 0; i < numTests; i++)
            {
                sw.Restart();
                col1.Contains(notExist);
                sw.Stop();
                time[0] += sw.ElapsedTicks;

                sw.Restart();
                scol1.Contains(strNotExist);
                sw.Stop();
                time[1] += sw.ElapsedTicks;

                sw.Restart();
                col2.ContainsValue(notExist);
                sw.Stop();
                time[2] += sw.ElapsedTicks;

                sw.Restart();
                scol2.ContainsValue(notExist);
                sw.Stop();
                time[3] += sw.ElapsedTicks;

                sw.Restart();
                col2.ContainsKey(notExistBase);
                sw.Stop();
                time[4] += sw.ElapsedTicks;

                sw.Restart();
                scol2.ContainsKey(strNotExistBase);
                sw.Stop();
                time[5] += sw.ElapsedTicks;
            }
            return time;
        }

        private void Fill()
        {
            col1.Clear();
            col2.Clear();
            scol1.Clear();
            scol2.Clear();

            for (int i = 0; i < stLen; i++)
            {
                Button button = new Button();
                ControlElement Base = button.GetBase;
                col1.Push(button);
                scol1.Push(button.ToString());
                col2.Add(Base, button);
                scol2.Add(Base.ToString(), button);
            }
        }
    }
}
