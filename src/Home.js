import React, { Component } from 'react';
import { Variable } from './variables';
export class Home extends Component {
  constructor(props) {
    super(props);
    this.state = {
      tasks: [],
      modal_title: '',
      task_title: '',
      completion: '',
      assigned_to: '',
      employees: [],
    };
  }

  refreshList() {
    fetch(Variable.API_URL + 'task')
      .then((response) => response.json())
      .then((data) => {
        this.setState({ tasks: data });
      });
    fetch(Variable.API_URL + 'employee')
      .then((response) => response.json())
      .then((data) => {
        this.setState({ employees: data });
      });
  }
  componentDidMount() {
    this.refreshList();
  }
  changeTitle = (e) => {
    this.setState({ task_title: e.target.value });
  };
  changeCompletion = (e) => {
    this.setState({ completion: e.target.value });
  };
  changeEmployee = (e) => {
    this.setState({ assigned_to: e.target.value });
  };

  addClick() {
    this.setState({
      modal_title: 'ADD TASK',
      task_title: '',
      completion: '',
      assigned_to: '',
    });
  }
  editClick(tsk) {
    this.setState({
      modal_title: 'Edit TASK',
      task_title: tsk.task_name,
      completion: tsk.completion_time,
      assigned_to: tsk.assigned_to,
    });
  }
  createClick() {
    fetch(Variable.API_URL + 'task', {
      method: 'POST',
      headers: {
        Accept: 'application/json',
        'Content-type': 'application/json',
        Charset: 'UTF-8',
      },
      body: JSON.stringify({
        task_title: this.state.task_title,
        completion: this.state.completion,
        assigned_to: this.state.assigned_to,
      }),
    })
      .then((res) => res.json())
      .then(
        (result) => {
          alert(result);
          this.refreshList();
        },
        (error) => {
          alert('Failed');
        }
      );
  }
  render() {
    const {
      employees,
      tasks,
      modal_title,
      task_title,
      completion,
      assigned_to,
    } = this.state;
    return (
      <div>
        <button
          type='button'
          className='btn btn-primary m-2 float-end'
          data-bs-toggle='modal'
          data-bs-target='#modal1'
          onClick={() => this.addClick()}
        >
          ADD TASK
        </button>
        <table className='table table-striped'>
          <thead>
            <tr>
              <th>Task Title</th>
              <th>Completion Time</th>
              <th>Assigned To</th>
              <th>Options</th>
            </tr>
          </thead>
          <tbody>
            {tasks.map((tsk) => (
              <tr key={tsk.task_name}>
                <td>{tsk.task_name}</td>
                <td>{tsk.completion_time}</td>
                <td>{tsk.assigned_to}</td>
                <button
                  type='button'
                  className='btn btn-light mr-1'
                  data-bs-toggle='modal'
                  data-bs-target='#modal1'
                  onClick={() => this.editClick(tsk)}
                >
                  <svg
                    xmlns='http://www.w3.org/2000/svg'
                    width='16'
                    height='16'
                    fill='currentColor'
                    class='bi bi-pencil-square'
                    viewBox='0 0 16 16'
                  >
                    <path d='M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z' />
                    <path
                      fillRule='evenodd'
                      d='M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5v11z'
                    />
                  </svg>
                </button>
                <button type='button' className='btn btn-light mr-1'>
                  <svg
                    xmlns='http://www.w3.org/2000/svg'
                    width='16'
                    height='16'
                    fill='currentColor'
                    class='bi bi-trash-fill'
                    viewBox='0 0 16 16'
                  >
                    <path d='M2.5 1a1 1 0 0 0-1 1v1a1 1 0 0 0 1 1H3v9a2 2 0 0 0 2 2h6a2 2 0 0 0 2-2V4h.5a1 1 0 0 0 1-1V2a1 1 0 0 0-1-1H10a1 1 0 0 0-1-1H7a1 1 0 0 0-1 1H2.5zm3 4a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 .5-.5zM8 5a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7A.5.5 0 0 1 8 5zm3 .5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 1 0z' />
                  </svg>
                </button>
              </tr>
            ))}
          </tbody>
        </table>
        <div
          className=' modal fade'
          id='modal1'
          tabIndex='-1'
          aria-hidden='true'
        >
          <div className='modal-dialog modal-lg modal-dialog-centered'>
            <div className='modal-content'>
              <div className='modal-header'>
                <h5 className='modal-title'>{modal_title}</h5>
                <button
                  type='button'
                  className='btn-close'
                  data-bs-dismiss='modal'
                  aria-label='Close'
                />
              </div>
              <div className='modal-body'>
                <div className='input-group mb-3'>
                  <span className='input-group-text'>Task Name</span>
                  <input
                    type='text'
                    className='form-control'
                    value={task_title}
                    onChange={this.changeTitle}
                  />
                </div>
                <div className='input-group mb-3'>
                  <span className='input-group-text'>Completion Time</span>
                  <input
                    type='text'
                    className='form-control'
                    value={completion}
                    onChange={this.changeCompletion}
                  />
                </div>
                <div className='input-group mb-3'>
                  <span className='input-group-text'>Assigned To</span>
                  <select
                    className='form-select'
                    value={assigned_to}
                    onChange={this.changeEmployee}
                  >
                    {employees.map((emp) => (
                      <option key={emp.employee_name} value={emp.employee_name}>
                        {emp.employee_name}
                      </option>
                    ))}
                  </select>
                </div>
                <button
                  onClick={() => this.createClick()}
                  type='button'
                  className='btn btn-primary float-start'
                >
                  Create
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>
    );
  }
}
