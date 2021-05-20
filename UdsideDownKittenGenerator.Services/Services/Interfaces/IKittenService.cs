using System.IO;
using System.Threading.Tasks;

namespace UdsideDownKittenGenerator.Services
{
    public interface IKittenService
    {
        Task<Stream> GetKitten();
    }
}
