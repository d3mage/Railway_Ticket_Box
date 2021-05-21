using System.Collections.Generic;

namespace DAL.Provider
{
    public interface IProvider<T>
    {
        void Write(List<T> data, string connection);
        List<T> Read(string connection);
    }
}
