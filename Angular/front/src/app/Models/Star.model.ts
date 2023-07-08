import { StarSystem } from "./star-system.model";

export enum TypeOfStar {
    MainSequenceStars = 'Main_sequence_stars',
    RedGiantStars = 'Red_giant_stars',
    WhiteDwarfStars = 'White_dwarf_stars',
    NeutronStars = 'Neutron_stars',
    BlackHoles = 'Black_holes'
  }
  
  export class Star {
    public type!: TypeOfStar;
    public temperature!: number;
    public radius!: number;
    public age!: number;
    public luminosity!: number;
    public mass!: number;
    public starSystemId!: number;
    public starSystem!: StarSystem;
  }