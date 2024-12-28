using RentRoomManagement.Common.Entitites.Location;
using RentRoomManagement.DL.Location;

namespace RentRoomManagement.BL.Location
{
    public class LocationBL
    {
        private readonly LocationDL _locationDL = new LocationDL();

        public async Task<IEnumerable<LocationDto>> GetAllLocations()
        {
            return await _locationDL.GetAllLocations();
        }
    }
}
