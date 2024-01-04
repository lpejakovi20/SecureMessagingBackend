using MrezeBackend.DTOs.AuthDTOs;

namespace MrezeBackend
{
    public class ClientManager
    {
        private static List<ClientBodyDTO> _clients = new List<ClientBodyDTO>();

        public static void SaveClient(ClientBodyDTO clientBodyDTO)
        {
            _clients.Add(clientBodyDTO);
        }

        public static ClientBodyDTO GetClient(string email)
        {
            return _clients.FirstOrDefault(x => x.Email == email);
        }
    }
}
