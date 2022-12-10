using SpaceWars;

namespace Interfaces
{
    public interface IEntityController
    {
        public void SetShootable(IShootable shootable);

        public void SetRotatable(IRotatable rotatable);

        public void SetMovable(IMovable movable);
    }
}