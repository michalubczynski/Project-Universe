import { Discoverer } from "./Discoverer.model";
import { Galaxy } from "./Galaxy.model";
import { Planet } from "./Planet.model";
import { Star } from "./Star.model";

export class StarSystem {
  public discoverers!: Discoverer[];
  public galaxy!: Galaxy;
  public galaxyId!: number;
  public id!: number;
  public name!: String;
  public planets!: Planet[];
  public stars!: Star[];
}