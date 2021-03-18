using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace screensharing_service.Hubs
{
    public class DomHub : Hub
    {
        

        public async Task sendDom(string user,string dom)
        {
            var i = 11;
            //
            await Clients.All.SendAsync("sentDom",user, dom);
        }
        
        
        public async Task sendMousePosition(string user,int x,int y)
        {
            await Clients.All.SendAsync("sentMousePosition",user, x,y);
        }

        public async Task sendScrollDown(string user)
        {
            await Clients.All.SendAsync("sentScrollDown",user);
        }
        
        public async Task sendScrollUp(string user)
        {
            await Clients.All.SendAsync("sentScrollUp",user);
        }

        public async Task sendScroll(string user,int vertical)
        {
            await Clients.All.SendAsync("sentScroll",user,vertical);
        }
        
    }
}