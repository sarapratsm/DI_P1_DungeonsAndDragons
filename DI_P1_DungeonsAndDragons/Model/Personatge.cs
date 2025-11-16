using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI_P1_DungeonsAndDragons.Model
{
    public class Personatge
    {
        private string nom;
        private Classe classe;
        private Species species;
        private string imageUrl;
        private float altura;
        private float pes;
        private int edat;
        private Alignment aligment;

        private static ObservableCollection<Personatge>? llistaPersonatges;

        public Personatge(string nom, Classe classe, Species species, string imageUrl)
        {
            this.nom = nom;
            this.classe = classe;
            this.species = species;
            this.imageUrl = imageUrl;
        }

        public Personatge(string nom, Classe classe, Species species, string imageUrl, float altura, float pes, int edat, Alignment alignment)
        {
            this.nom = nom;
            this.classe = classe;
            this.species = species;
            this.imageUrl = imageUrl;
            this.altura = altura;
            this.pes = pes;
            this.edat = edat;
            this.aligment = alignment;

        }

        public string Nom { get => nom; set => nom = value; }
        public Classe Classe { get => classe; set => classe = value; }
        public Species Species { get => species; set => species = value; }
        public string ImageUrl { get => imageUrl; set => imageUrl = value; }

        #region ObservableCollection de personatges

        public static ObservableCollection<Personatge> GetLlistaPersonatges
        {
            get
            {
                if (llistaPersonatges == null)
                {
                    llistaPersonatges = new ObservableCollection<Personatge>();

                    var classes = Classe.GetClasses();
                    var species = Species.GetSpecies();
                    var alignments = Alignment.GetAlignments();

                    // Afegir dades de mostra 
                    llistaPersonatges.Add(new Personatge(
                        "Elzha",
                        classes.First(c => c.Name == "Bard"),
                        species.First(s => s.Name == "Tiefling"),
                        "https://i.pinimg.com/736x/e0/77/df/e077df94c74cff4232935f2ff43d8f84.jpg",
                        2.10f, 120f, 32,
                        alignments.First(a => a.Name == "Chaotic Good")
                    ));

                    llistaPersonatges.Add(new Personatge(
                        "Elandra Moonwhisper",
                        classes.First(c => c.Name == "Druid"),
                        species.First(s => s.Name == "Elf"),
                        "https://i.pinimg.com/736x/7e/dd/97/7edd97573c5967d4b2b41b25824ad3cb.jpg",
                        1.70f, 55f, 140,
                        alignments.First(a => a.Name == "Neutral Good")
                    ));

                    llistaPersonatges.Add(new Personatge(
                        "Nyx Shadowstep",
                        classes.First(c => c.Name == "Rogue"),
                        species.First(s => s.Name == "Human"),
                        "https://i.pinimg.com/736x/03/ca/c7/03cac76a5c22ec21e8251992fc833f99.jpg",
                        1.68f, 52f, 27,
                        alignments.First(a => a.Name == "Chaotic Neutral")
                    ));

                    llistaPersonatges.Add(new Personatge(
                        "Lady Aldrena Brightshield",               // Nom femení
                        classes.First(c => c.Name == "Paladin"),
                        species.First(s => s.Name == "Human"),
                        "https://i.pinimg.com/1200x/01/9e/49/019e49b497772e25d771e3533b733837.jpg",
                        1.75f, 70f, 38,                             // Altura i pes ajustats
                        alignments.First(a => a.Name == "Lawful Good")
                    ));

                    llistaPersonatges.Add(new Personatge(
                        "Cobol the Silent",
                        classes.First(c => c.Name == "Paladin"),
                        species.First(s => s.Name == "Dragonborn"),
                        "https://i.pinimg.com/736x/ec/6c/a1/ec6ca183988b0d27a1f7164ad719cd55.jpg",
                        1.83f, 80f, 38,
                        alignments.First(a => a.Name == "Lawful Neutral")
                    ));
                    llistaPersonatges.Add(new Personatge(
                        "Kaleia",
                        classes.First(c => c.Name == "Druid"),
                        species.First(s => s.Name == "Tiefling"),
                        "https://i.pinimg.com/736x/21/e9/ea/21e9ea10a74996eeaf72eea2690a7655.jpg",
                        1.45f, 70f, 112,
                        alignments.First(a => a.Name == "Lawful Good")
                    ));
                    llistaPersonatges.Add(new Personatge(
                        "Zorith Malqir",
                        classes.First(c => c.Name == "Bard"),
                        species.First(s => s.Name == "Dwarf"),
                        "https://i.pinimg.com/1200x/68/fb/cb/68fbcbdb2e6c79e7a01b2832aa95c21d.jpg",
                        1.45f, 70f, 112,
                        alignments.First(a => a.Name == "Lawful Good")
                    ));

                    llistaPersonatges.Add(new Personatge(
                        "Ennerys",
                        classes.First(c => c.Name == "Wizard"),
                        species.First(s => s.Name == "Aasimar"),
                        "https://i.pinimg.com/736x/31/dd/8e/31dd8efcf7d6e313e7f5960b266b8c22.jpg",
                        1.10f, 35f, 90,
                        alignments.First(a => a.Name == "Neutral Good")
                    ));

         

                    llistaPersonatges.Add(new Personatge(
                        "Mordek Kragthorn",
                        classes.First(c => c.Name == "Warlock"),
                        species.First(s => s.Name == "Gnome"),
                        "https://i.pinimg.com/736x/bd/1d/86/bd1d86525f5c2832475cb7c5c92d7b6c.jpg",
                        1.77f, 68f, 56,
                        alignments.First(a => a.Name == "Neutral Evil")
                    ));

                    llistaPersonatges.Add(new Personatge(
                        "Tara Greenbottle",
                        classes.First(c => c.Name == "Ranger"),
                        species.First(s => s.Name == "Elf"),
                        "https://i.pinimg.com/474x/c6/d0/50/c6d0503af429e22f36ff22b42d5739d4.jpg",
                        0.98f, 28f, 33,
                        alignments.First(a => a.Name == "Chaotic Good")
                    ));




                }

                return llistaPersonatges;
            }
        }
        #endregion
    }
}