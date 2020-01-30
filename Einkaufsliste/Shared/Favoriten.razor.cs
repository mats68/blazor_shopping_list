using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;


namespace Einkaufsliste.Shared
{
    public class FavoritenBase : ComponentBase
    {
        [Inject]
        IEinkaufService EinkaufSrv { get; set; }

        public List<Einkauf> FavoritenList
        {
            get
            {
                return EinkaufSrv.Favoriten;
            }
        }

        public string newFavorit;

        protected override async Task OnInitializedAsync()
        {
            await EinkaufSrv.GetFavoriten();
        }

        public Einkauf CurrentItem
        {
            get { return EinkaufSrv.CurrentFavorit; }
            set { EinkaufSrv.CurrentFavorit = value; }
        }


        public async Task AddFavorit()
        {
            await Task.Run(() => EinkaufSrv.AddFavorit(new Einkauf { Name = newFavorit }));
            newFavorit = string.Empty;
        }

        public void SetCurrent(Einkauf fav)
        {
            CurrentItem = fav;
        }
        
        public void Up()
        {
            if (EinkaufSrv.CurrentFavorit != null)
            {
                EinkaufSrv.Up(FavoritenList, EinkaufSrv.CurrentFavorit);
            }
        }

        public void Down()
        {
            if (EinkaufSrv.CurrentFavorit != null)
            {
                EinkaufSrv.Down(FavoritenList, EinkaufSrv.CurrentFavorit);
            }
        }

        public string GetActiveClass(Einkauf fav)
        {
            return CurrentItem == fav ? "list-group-item active" : "list-group-item";
        }
    }
}
