import './App.css';
import { Home } from './Home';
import { Admin } from './Admin';
import { Employee } from './Employee';
import {
  BrowserRouter as Router,
  Route,
  Switch,
  NavLink,
} from 'react-router-dom';

function App() {
  return (
    <Router>
      <div className='App container'>
        <h3 className='d-flex justify-content-center m-3'>React js FrontEnd</h3>

        <nav className='navbar navbar-expand-sm bg-light navbar-dark'>
          <ul className='navbar-nav'>
            <li className='nav-item- m-1'>
              <NavLink className='btn btn-light btn-outline-primary' to='/Home'>
                TASK
              </NavLink>
            </li>
            <li className='nav-item- m-1'>
              <NavLink
                className='btn btn-light btn-outline-primary'
                to='/Admin'
              >
                ADMIN
              </NavLink>
            </li>
            <li className='nav-item- m-1'>
              <NavLink
                className='btn btn-light btn-outline-primary'
                to='/Employee'
              >
                EMPLOYEE
              </NavLink>
            </li>
          </ul>
        </nav>

        <Switch>
          <Route path={'/Home'} component={Home} />
          <Route path='/Admin' component={Admin} />
          <Route path='/Employee' component={Employee} />
        </Switch>
      </div>
    </Router>
  );
}

export default App;
