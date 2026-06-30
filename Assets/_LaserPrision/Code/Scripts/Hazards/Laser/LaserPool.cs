using LaserPrison.Pooling;

namespace LaserPrison.Hazards
{
    public class LaserPool : ObjectPoolController<Laser>
    {
        protected override Laser CreateObject()
        {
            Laser laser = base.CreateObject();

            laser.SetReleaseAction(Release);

            return laser;
        }
    }
}