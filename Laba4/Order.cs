using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Laba4
{
    [Serializable]
    public struct Order : IComparable<Order>
    {
        public ulong payAccount;
        public ulong receiveAccount;
        public ulong sum;

        public Order(string lineWithAllData)
        {
            string[] value = Convert.ToString(lineWithAllData).Trim().Split();
            payAccount = Convert.ToUInt64(value[0]);
            receiveAccount = Convert.ToUInt64(value[1]);
            sum = Convert.ToUInt64(value[2]);
        }
        public int CompareTo(Order that)
        {
            ulong thisSum = this.sum;
            ulong thatSum = that.sum;
            if (thisSum == thatSum)
            {
                return 0;
            }
            else if (thisSum > thatSum)
            {
                return -1;
            }
            else
            {
                return +1;
            }
        }
        public string ToString()
        {
            return $"{payAccount} {receiveAccount} {sum}";
        }
        public string Show()
        {
            ulong whole = sum / 100;
            ulong part = sum % 100;
            return $"------------------------------------------------\nPayer's current account: {payAccount}. \nRecipient's current account: {receiveAccount}. \nTransfer amount: {whole}.{part} UAH\n------------------------------------------------";
        }
    }
}
