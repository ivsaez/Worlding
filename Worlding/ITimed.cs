using Identification;
using Outputer;

namespace Worlding
{
    public interface ITimed : IIdentifiable
    {
        Output OnTurnPassed(IWorld world, uint turns);
    }

    public delegate Output TurnPassed(IWorld world, uint turns);
}
