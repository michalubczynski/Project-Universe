import { Ship } from "./Ship.model";
import { StarSystem } from "./star-system.model";

export class Discoverer {
    public surname!: string;
    public age!: number;
    public shipId!: number;
    public ship!: Ship;
    public starSystems!: StarSystem[];
  }