using System.Threading;
using System.Threading.Tasks;

namespace Events.ItAcademy.Ge.CMVC
{
    public interface IWorker
    {
        Task DoWorkAsync(CancellationToken cancellation);
    }
}