using Outputer;

namespace Worlding
{
    public interface ITimed
    {
        Output OnTurnPassed(IWorld world, int turns);
    }
}
