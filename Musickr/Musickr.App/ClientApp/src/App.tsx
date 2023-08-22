import React from 'react';
import { Route, Routes } from 'react-router-dom';
import AppRoutes from './AppRoutes';
import Layout from './components/Layout';
import './custom.css';
import {ChakraProvider} from "@chakra-ui/react";
import {QueryClient, QueryClientProvider} from "react-query";

const queryClient = new QueryClient();
const App : React.FC = () => {
  return (
    <QueryClientProvider client={queryClient}>
      <ChakraProvider>
        <Layout>
          <Routes>
            {AppRoutes.map((route, index) => {
              const { element, ...rest } = route;
              return <Route key={index} {...rest} element={element} />;
            })}
          </Routes>
        </Layout>
      </ChakraProvider>
    </QueryClientProvider>
  );
};

export default App;