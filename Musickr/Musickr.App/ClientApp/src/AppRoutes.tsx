import SearchPage from "./components/Search/Pages/SearchPage";
import PlayerPage from "./components/Player/Pages/PlayerPage";

const AppRoutes = [
  {
    index: true,
    element: <SearchPage />
  },
  {
    path: "/player",
    element: <PlayerPage />
  }
];

export default AppRoutes;
