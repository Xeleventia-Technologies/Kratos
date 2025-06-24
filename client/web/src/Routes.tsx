import type { JSX } from "react";
import { Routes as Routes_, Route } from "react-router-dom";
import { routes, type RouteItem } from "@/constants/routes";

function buildRoutes(routes: RouteItem[], elements: JSX.Element[]): void {
  for (const route of routes) {
    const routeElement = route.element;

    elements.push(
      <Route key={route.path} path={route.path} element={routeElement} />
    );

    if (route.children) {
      buildRoutes(route.children, elements);
    }
  }
}

export function Routes() {
  const routeList: JSX.Element[] = [];
  buildRoutes(routes, routeList);

  return <Routes_>{routeList}</Routes_>;
}
