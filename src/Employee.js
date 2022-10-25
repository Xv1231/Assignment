import React, { Component } from 'react';
import { Variable } from './variables';

export class Employee extends Component {
  constructor(props) {
    super(props);
    this.state = {
      employees: [],
    };
  }
  refreshList() {
    fetch(Variable.API_URL + 'employee')
      .then((response) => response.json())
      .then((data) => {
        this.setState({ employees: data });
      });
  }
  componentDidMount() {
    this.refreshList();
  }
  render() {
    const { employees } = this.state;
    return (
      <div>
        <table className='table table-striped'>
          <thead>
            <tr>
              <th>Employee Name</th>
              <th>Employee Department</th>
              <th>Date of Joining</th>
            </tr>
          </thead>
          <tbody>
            {employees.map((emp) => (
              <tr key={emp.employee_name}>
                <td>{emp.employee_name}</td>
                <td>{emp.employee_department}</td>
                <td>{emp.date_of_joining}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    );
  }
}
