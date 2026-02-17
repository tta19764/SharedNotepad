import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import 'bootstrap/dist/css/bootstrap.min.css'
import App from './App.tsx'
import { createBrowserRouter, RouterProvider } from 'react-router-dom'
import NotePage from './pages/NotePage.tsx'
import NotFoundPage from './pages/NotFoundPage.tsx'
import EmptyPage from './pages/EmptyPage.tsx'

const router = createBrowserRouter([
  {
    path: '/',
    element: <App />,
    children: [
      {
        path: '',
        element: <EmptyPage />
      },
      {
        path: ':id',
        element: <NotePage />
      },
      {
        path: '*',
        element: <NotFoundPage />
      }
    ]
  },
])

createRoot(document.getElementById('root')!).render(
  <StrictMode>
      <RouterProvider router={router} />
  </StrictMode>
);
