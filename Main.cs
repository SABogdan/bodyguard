
using CitizenFX.Core;
using CitizenFX.Core.Native;
using System;
using System.Threading.Tasks;

namespace Bodyguard
{
    public class Main :BaseScript
    {
        public Main()
        {
            API.RegisterCommand("bodyguard", new Action(spawnBodyguard), false);
        }
        private async static void spawnBodyguard()
        {
            try { 
            Ped player = Game.Player.Character;
            API.RequestModel((uint)PedHash.ChemSec01SMM);
            while (!API.HasModelLoaded((uint)PedHash.ChemSec01SMM))
            {
                Debug.WriteLine("waiting on model to load");
                await BaseScript.Delay(100);
            }
            Ped bodyguard = await World.CreatePed(PedHash.ChemSec01SMM, player.Position + (player.ForwardVector * 2));
            bodyguard.Task.LookAt(player);
            API.SetPedAsGroupMember(bodyguard.Handle, API.GetPedGroupIndex(player.Handle));
            API.SetPedCombatAbility(bodyguard.Handle, 2);
            API.GiveWeaponToPed(bodyguard.Handle, (uint)WeaponHash.SpecialCarbineMk2, 500, false, false);
            API.SetBlipFriendly(API.AddBlipForEntity(bodyguard.Handle), true);
            bodyguard.PlayAmbientSpeech("GENERIC_HI");
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"{ex}");
            }
        }

    }
}
