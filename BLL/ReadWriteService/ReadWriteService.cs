using DAL;
using System.Collections.Generic;

namespace BLL
{
    public class ReadWriteService<T, Y> : IReadWriteService<T, Y>
    {
        private IDataContext<Y> _dataContext;
        public ReadWriteService(IDataContext<Y> context)
        {
            _dataContext = context;
        }
        public List<T> ReadData()
        {
            List<T> readData = new List<T>();
            try
            {
                ConversionService.ConversionService<Y, T>.conversion(_dataContext.GetData(), readData);
            }
            catch (EmptyListException)
            {
                readData = new List<T>();
            }
            return readData;
        }
        public void WriteData(List<T> data)
        {
            List<Y> writeData = new List<Y>();
            ConversionService.ConversionService<T, Y>.conversion(data, writeData); 
            _dataContext.SetData(writeData);
        }
    }
}
