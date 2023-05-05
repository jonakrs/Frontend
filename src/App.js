import * as React from 'react';
import { DataGrid } from '@material-ui/data-grid';
// import axios from 'axios';

const columns = [
  { field: 'id', headerName: 'ID', width: 90 },
  {
    //firstname,lastname,age and other columns and rows will be replaced with json data from the endpoint
    field: 'firstName',
  
    headerName: 'First name',
    width: 150,
    editable: true,
  },
  {
    field: 'lastName',
    headerName: 'Last name',
    width: 150,
    editable: true,
  },
  {
    field: 'age',
    headerName: 'Age',
    type: 'number',
    width: 110,
    editable: true,
  },
  {
    field: 'fullName',
    headerName: 'Full name',
    description: 'This column has a value getter and is not sortable.',
    sortable: false,
    width: 160,
    valueGetter: (params) =>
      `${params.getValue(params.id, 'firstName') || ''} ${
        params.getValue(params.id, 'lastName') || ''
      }`,
  
},
];
//these data will be replaced with json data
const rows = [
  { id: 1, lastName: 'Blenda', firstName: 'Blenda', age: 35 },
  { id: 2, lastName: 'Krasniqi', firstName: 'Jona', age: 42 },
  { id: 3, lastName: 'Lipa', firstName: 'Dua', age: 28 },
  { id: 4, lastName: 'Ora', firstName: 'Rita', age: 16 },
  { id: 5, lastName: 'Rexha', firstName: 'Bebe', age: 32 },
  { id: 6, lastName: 'Gashi', firstName: 'Aleks', age: 55 },
];

export default function DataTable() {
  return (
    <div style={{ height: 400, width: '100%' }}>
      <DataGrid
        rows={rows}
        columns={columns}
        pageSize={5}
        checkboxSelection
        disableSelectionOnClick
      />
    </div>
  );
}
