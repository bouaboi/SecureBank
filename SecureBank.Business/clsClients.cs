using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using SecureBank.DataAccess;
using SecureBank.Models;

namespace SecureBank.Business
{
    public class clsClients
    {

        public static DataTable GetAllClients()
        {
           return clsClientsData.GetAllClients();   
        }

        public static int AddNewClient(Client client)
        {
            return clsClientsData.AddNewClient(client);
        }

        public static Client GetClientById(int clientId)
        {
            return clsClientsData.GetClientById(clientId);
        }
        public static bool UpdateClient(Client client)
        {
            return clsClientsData.UpdateClient(client);
        }


        public static bool SoftDeleteClient(int client)
        {
            return clsClientsData.SoftDeleteClient(client);
        }

        public static bool ActivateClient(int client)
        {
            return clsClientsData.ActivateClient(client);
        }


    }
}