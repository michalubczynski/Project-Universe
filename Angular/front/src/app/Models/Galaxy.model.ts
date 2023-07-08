import { StarSystem } from "./star-system.model";

export enum TypeOfGalaxy {
    Spiral = 'spiral',
    Elliptical = 'elliptical',
    Peculiar = 'peculiar',
    Irregular = 'irregular'
  }
  
  export class Galaxy {
    public type!: TypeOfGalaxy;
    public mass!: number;
    public starSystems!: StarSystem[];
  }