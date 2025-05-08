import React from 'react';
import './styles/about.css';
import LOGO from './img/Logoo.png';

export function About() {
    return (
        <nav className="About-page">
            <div className="page-wrapper">
                <div className="about-card">
                    <p className="about-lead">
                        Välkommen till SoleMate, din destination för att enkelt och tryggt köpa sneakers online.
                    </p>
                    <p className="about-lead">
                        Vårt mål är att erbjuda en säker och smidig plattform där sneaker-entusiaster kan hitta de senaste modellerna och tidlösa klassiker. Vi strävar efter att göra det enkelt för dig att upptäcka, köpa och njuta av högkvalitativa skor från kända varumärken.
                    </p>
                    <p className="about-lead">
                        Vi uppdaterar ständigt vårt sortiment med nya produkter för att säkerställa att du alltid har tillgång till de bästa skorna på marknaden. Oavsett om du letar efter sportiga sneakers, trendiga vardagsskor eller bekväma löparskor, har vi något för dig.
                    </p>
                    <p className="about-lead">
                        Vår plattform är utformad med fokus på användarvänlighet och säkerhet, så att du kan handla med fullständig trygghet. Vi arbetar kontinuerligt med att förbättra vår tjänst och ser fram emot att erbjuda dig den bästa shoppingupplevelsen.
                    </p>
                    <p className="about-lead">
                        Tveka inte att kontakta oss om du har frågor eller behöver hjälp. Välkommen till SoleMate – där stil och komfort möts!
                    </p>

                    <img src={LOGO} alt="LOGO" className="img-fluid" />
                </div>
            </div>
        </nav>
    );
}
export default About;
