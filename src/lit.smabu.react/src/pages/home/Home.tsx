import { AuthenticatedTemplate, UnauthenticatedTemplate } from "@azure/msal-react";
import { useAuth } from "../../contexts/authContext";

export function Home() {

  const { account } = useAuth(); 

  return (
      <>
        <div>AuthToken: {useAuth().accessToken}</div>
          <AuthenticatedTemplate>
            <div>Willkommen, {account?.name}!</div>
          </AuthenticatedTemplate>

          <UnauthenticatedTemplate>
            <div>Bitte anmelden</div>
          </UnauthenticatedTemplate>
      </>
  );
}