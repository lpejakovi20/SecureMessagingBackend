using MrezeBackend.DTOs.AuthDTOs;

namespace MrezeBackend
{
    public class ClientManager
    {
        private static List<ClientRegisterBodyDTO> _clients = new List<ClientRegisterBodyDTO>();

        public static void SaveClient(ClientRegisterBodyDTO clientBodyDTO)
        {
            _clients.Add(clientBodyDTO);
        }

        public static ClientRegisterBodyDTO GetClient(string email)
        {
            return _clients.FirstOrDefault(x => x.Email == email);
        }
    }
}
