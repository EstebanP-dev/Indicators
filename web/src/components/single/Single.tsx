import { Box } from "@mui/material";
import "./single.scss"

type Props = {
  id: number | string;
  img?: string;
  title: string;
  info: object;
}

const Single = (props: Props) => {
  // https://images.pexels.com/photos/5791753/pexels-photo-5791753.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1
  return (
    <div className="single">
      <div className="view">
        <div className="info">
          <div className="topinfo">
            {
              props.img
              &&
              <img src={props.img} alt="Avatar image" />
            }
            <h1>{props.title}</h1>
            <button>Actualizar</button>
          </div>
          <div className="details">
            {
              Object.entries(props.info).map((item) => (
                <div className="item" key={item[0]}>
                  <span className="itemTitle">{item[0]}:</span>
                  <span className="itemValue">{item[1]}</span>
                </div>
              ))
            }
            
          </div>
        </div>
        <hr />
        <div className="chart">
          <Box height={300} width={500} />
        </div>
      </div>
      <div className="activities">
        <h2>Ultimas actividades</h2>
        <ul>
          <li>
            <div>
              <p>Actividad de prueba</p>
              <time>Hace 4 días</time>
            </div>
          </li>
          <li>
            <div>
              <p>Actividad de prueba</p>
              <time>Hace 4 días</time>
            </div>
          </li>
          <li>
            <div>
              <p>Actividad de prueba</p>
              <time>Hace 4 días</time>
            </div>
          </li>
          <li>
            <div>
              <p>Actividad de prueba</p>
              <time>Hace 4 días</time>
            </div>
          </li>
        </ul>
      </div>
    </div>
  )
}

export default Single