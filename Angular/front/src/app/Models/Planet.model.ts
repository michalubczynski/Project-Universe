import { StarSystem } from "./star-system.model";

export enum TypeOfPlanets {
    GasGiants = 'Gas_Giants',
    DwarfPlanets = 'Dwarf_Planets',
    SuperEarth = 'Super_Earth',
    TerrestrialPlanets = 'Terrestrial_Planets',
    OuterPlanets = 'Outer_Planets'
  }
  
  export class Planet {
    public type!: TypeOfPlanets;
    public mass!: number;
    public starSystemId!: number;
    public starSystem!: StarSystem;
  }