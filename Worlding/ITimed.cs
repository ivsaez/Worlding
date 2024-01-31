using Outputer;

namespace Worlding
{
    public interface ITimed
    {
        Output OnTurnPassed(int turns);
    }
}
