using System;
using System.Collections.Generic;

namespace FactorioProductionCells.TestData.Common
{
    public class CommonTestData
    {
        public static List<Int32> PositiveIntegerValues = new List<Int32> { 0, 1, 2, Int32.MaxValue };
        
        // Note: Arbitrary values were selected for these reasons:
        //    * 2024-04-15 16:56:37.623 - This is simply an arbitrary date in the future.
        //    * 2012-02-29 02:14:54.223 - This date is on a leap year day.
        //    * 2015-11-01 01:43:06.997 - This date is during an hour lost (or "doesn't happen") due to daylight savings time.
        //    * 2017-03-12 02:30:00.000 - This date is during an hour gained (or "repeated") due to daylight savings time.
        //    * 2000-01-01 00:00:00.000 - This date was included because Y2K was scary, and this code base needs more memes.
        //    * 2038-01-19 03:14:08.000 - This date was included because it's past the maximum value of 32-bit UTC, and that's way scarier than Y2K.
        public static List<DateTime> DateTimeValues = new List<DateTime>
        {
            DateTime.MinValue,
            DateTime.UnixEpoch,
            DateTime.Parse("2000-01-01 00:00:00.000"),
            DateTime.Parse("2012-02-29 02:14:54.223"),
            DateTime.Parse("2015-11-01 01:43:06.997"),
            DateTime.Parse("2017-03-12 02:30:00.000"),
            DateTime.Parse("2024-04-15 16:56:37.623"),
            DateTime.Parse("2038-01-19 03:14:08.000"),
            DateTime.Now,
            DateTime.MaxValue
        };

        public static IEnumerable<object[]> VariousValueTypeData =>
            new List<object[]>
            {
                new object[] { (SByte)(-16) },
                new object[] { (Byte)255 },
                new object[] { (Int16)(-16) },
                new object[] { (UInt16)16 },
                new object[] { (Int32)(-32) },
                new object[] { (UInt32)32 },
                new object[] { (Int64)(-64) },
                new object[] { (UInt64)64 },
                new object[] { (Char)'Z' },
                new object[] { (Single)12.34 },
                new object[] { (Double)Single.MaxValue * 2 },
                new object[] { (Boolean)true },
                new object[] { (Decimal)79228162514264337.54542245 },
                new object[] { (String)"dQw4w9WgXcQ" },
                new object[] { Guid.NewGuid() }
            };

        public static IEnumerable<object[]> EmptyAndWhitespaceStrings =>
            new List<object[]>
            {
                new object[] { "" },
                new object[] { " " },
                new object[] { "    " },
                new object[] { "\r" },
                new object[] { "\n" },
                new object[] { "\t" },
                new object[] { "\v" }
            };
    }
}
