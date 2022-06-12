using System.Threading.Tasks;

namespace homecoming.api.Interfaces
{
    public interface IMultiFileUpload<T>
    {
        public bool MultiFileUpload(T file);
    }
}
