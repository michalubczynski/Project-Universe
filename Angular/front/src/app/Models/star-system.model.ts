import { Discoverer } from "./Discoverer.model";
import { Galaxy } from "./Galaxy.model";
import { Planet } from "./Planet.model";
import { Star } from "./Star.model";

export class StarSystem {
  public galaxyId!: number;
  public galaxy!: Galaxy;
  public planets: Planet[] = [];
  public stars: Star[] = [];
  public discoverers: Discoverer[] = [];
  public name!:string;
}