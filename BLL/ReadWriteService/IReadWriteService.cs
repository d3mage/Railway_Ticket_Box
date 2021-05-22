using DAL;
using System.Collections.Generic;

namespace BLL
{
    public interface IReadWriteService<T, Y>
    {
        public List<T> ReadData();
        public void WriteData(List<T> data);
    }
}
