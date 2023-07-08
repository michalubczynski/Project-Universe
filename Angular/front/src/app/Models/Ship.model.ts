import { Discoverer } from "./Discoverer.model";

export class Ship {
    public name!: string;
    public shipModel!: string;
    public maxSpeed!: number;
    public singleChargeRange!: number;
    public discoverer!: Discoverer;
    public ifBroken!: boolean;
  }